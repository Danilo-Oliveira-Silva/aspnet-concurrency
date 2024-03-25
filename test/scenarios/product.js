import http from 'k6/http';
import { group, sleep, check, fail } from 'k6';
import { Trend } from 'k6/metrics';

const uptimeTrendCheck = new Trend('get_products_response_time');

export const options = {
  vus: 1,
  duration: '20s',
  iterations: 1
};

export function setup() {
  const url = "http://localhost:5000/product";
  const payload = JSON.stringify({
    Name: 'ProdutoA',
    Stock: 30
  });

  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  const res = http.post(url, payload, params);
  const productCreated = JSON.parse(res.body).guid;
  return { 'productCreated': productCreated };
}

export default function(data) {
  group('API check', () => {
    const response = http.get('http://localhost:5000/product');
    uptimeTrendCheck.add(response.timings.duration);
    check(response, {
        "status code should be 200": res => res.status === 200,
    });

    check(response, {
       "product should be the same": res => JSON.parse(res.body)[0].guid == data['productCreated'],
    });
  });

  group('Stock check', () => {

    const url = `http://localhost:5000/invoice/remove/${data['productCreated']}/30`;
    const response = http.patch(url);
    check(response, {
      "status code should be 200": res => res.status === 200,
    });

    const urlb = `http://localhost:5000/invoice/remove/${data['productCreated']}/1`;
    const responseb = http.patch(urlb);
    check(responseb, {
      "status code should be 409": res => res.status === 409,
    });

  });
}
