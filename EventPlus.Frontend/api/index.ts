import axios from "axios";
import { errorInterceptorError, errorInterceptorFulfilled } from "./interceptors/errorInterceptor";
import { jwtInterceptor } from "./interceptors/jwtInterceptor";
import { currentCommandInterceptor } from "./interceptors/currentCommandInterceptor";
import { baseAxiosConfiguration } from "./_base";

export const _apiBase = axios.create(baseAxiosConfiguration);

_apiBase.interceptors.request.use(jwtInterceptor);
_apiBase.interceptors.request.use(currentCommandInterceptor);

_apiBase.interceptors.response.use(
  errorInterceptorFulfilled,
  errorInterceptorError
);