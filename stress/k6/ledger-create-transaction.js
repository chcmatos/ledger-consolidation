import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  scenarios: {
    write_load: {
      executor: 'constant-arrival-rate',
      rate: 10,
      timeUnit: '1s',
      duration: '1m',
      preAllocatedVUs: 20,
      maxVUs: 100,
    }
  },
  thresholds: {
    http_req_failed: ['rate<0.05'],
  }
};

export default function () {
  const url = 'http://localhost:5101/transactions';
  const payload = JSON.stringify({
    businessDate: '2026-01-11',
    amount: 1.23,
    type: 'Credit',
    description: 'k6'
  });
  const params = { headers: { 'Content-Type': 'application/json' } };

  const res = http.post(url, payload, params);
  check(res, { '201': (r) => r.status === 201 });
  sleep(0.1);
}
