import http from 'k6/http';
import { group, sleep, check, fail } from 'k6';
import { Trend } from 'k6/metrics';

const uptimeTrendCheck = new Trend('get_products_response_time');

export const options = {
  vus: 2,
  duration: '60s',
  iterations: 1000
};

export function setup() {
  const url = "http://localhost:5000/product";
  const payload = JSON.stringify({
    Name: 'ProdutoA',
    Stock: 0
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


  group('Stock add', () => {

    const url = `http://localhost:5000/invoice/add/${data['productCreated']}/1`;
    const response = http.patch(url);
    check(response, {
      "status code should be 200": res => res.status === 200,
    });
  });
}
