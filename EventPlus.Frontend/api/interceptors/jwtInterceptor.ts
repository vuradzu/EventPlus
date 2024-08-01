import { InternalAxiosRequestConfig } from "axios";
import { JwtHelper } from "~/utils/helpers/jwtHelper";

export const jwtInterceptor = (request: InternalAxiosRequestConfig<any>) => {
  const tokenInfo = JwtHelper.getTokenInfo();

  if (!!tokenInfo) {
    request.headers.Authorization = `Bearer ${tokenInfo.token}`;
  }

  return request;
};
