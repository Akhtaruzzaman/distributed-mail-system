import axios, { AxiosRequestConfig } from 'axios';
import { Observable } from 'rxjs';
import { from } from 'rxjs';
import { ApiUrl } from '../constant/url';

// Axios instance configuration
const api = axios.create({
  baseURL: ApiUrl.baseUrl,
  timeout: 10000,
});

// Helper function to create an Observable from an Axios request
const axiosRequest = (config: AxiosRequestConfig): Observable<any> => {
  return from(api.request(config).then(response => response.data));
};

export const get = (url: string): Observable<any> => {
  return axiosRequest({ method: 'GET', url });
};

export const post = (url: string, data: any): Observable<any> => {
  return axiosRequest({ method: 'POST', url, data });
};

export const put = (url: string, data: any): Observable<any> => {
  return axiosRequest({ method: 'PUT', url, data });
};

export const del = (url: string): Observable<any> => {
  return axiosRequest({ method: 'DELETE', url });
};
