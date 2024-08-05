import { InternalAxiosRequestConfig } from "axios";
import { router } from "expo-router";
import { JwtHelper } from "~/utils/helpers/jwtHelper";

export const jwtInterceptor = async (
  request: InternalAxiosRequestConfig<any>
) => {
  let accessToken: string | undefined = "";

  const tokenInfo = JwtHelper.getTokenInfo();

  if (!tokenInfo) {
    router.replace("sign-in");
    return;
  }

  const tokenValid = !JwtHelper.isTokenExpired(tokenInfo.tokenExpires);

  if (!tokenValid) {
    const refreshedToken = await JwtHelper.refreshToken();
    accessToken = refreshedToken?.token;
  } else {
    accessToken = tokenInfo.token;
  }

  if (!!accessToken) {
    request.headers.Authorization = `Bearer ${accessToken}`;
  }

  return request;
};
