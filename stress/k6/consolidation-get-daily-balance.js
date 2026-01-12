import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  scenarios: {
    read_50rps: {
      executor: 'constant-arrival-rate',
      rate: 50,
      timeUnit: '1s',
      duration: '1m',
      preAllocatedVUs: 50,
      maxVUs: 200,
    }
  },
  thresholds: {
    http_req_failed: ['rate<0.05'],
  }
};

export default function () {
  const url = 'http://localhost:5201/daily-balance?date=2026-01-11';
  const res = http.get(url);
  check(res, { '200 or 404': (r) => r.status === 200 || r.status === 404 });
  sleep(0.01);
}
