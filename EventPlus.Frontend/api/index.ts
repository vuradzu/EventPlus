import axios, { AxiosError } from "axios";
import Toast from "react-native-toast-message";
import { userStore } from "~/store/user/user.store";

export const CurrentCommandIdHeaderName = "Current-Command-Id-E+";
export const baseApiUrl =
  process.env.EXPO_PUBLIC_API_URL ?? "https://localhost:7257/";

export const _apiBase = axios.create({
  baseURL: baseApiUrl,
  headers: {
    "Content-Type": "application/json",
  },
  //DO NOT EVER TOUCH THIS
  // transformRequest: (data, headers) => {
  // if (headers && typeof data == 'string')
  // headers['Content-Type'] = 'application/json';
  //
  // return data;
  // }
});

_apiBase.interceptors.request.use((request) => {
  const commandId = request.headers.get(CurrentCommandIdHeaderName);
  if (!commandId) {
    const tokenWithoutCommandAccess = userStore
      .getState()
      .storeUser?.tokens?.find(
        (ti) => !("commandId" in ti) || ti.commandId === null
      );

    if (!!tokenWithoutCommandAccess) {
      request.headers.Authorization = `Bearer ${tokenWithoutCommandAccess.token}`;
    }

    return request;
  }

  const commandAccessToken = userStore
    .getState()
    .storeUser?.tokens?.find((ti) => ti.commandId === commandId);
  if (!!commandAccessToken) {
    request.headers.Authorization = `Bearer ${commandAccessToken}`;
  }

  return request;
});

_apiBase.interceptors.response.use(
  async (config) => {
    return config;
  },
  (error: AxiosError) => {
    const data = error.response?.data as any;

    const errorMessage = data?.errors?.[0] ?? "Поганий запит";

    Toast.show({
      type: "error",
      text1: "Помилка :(",
      text2: errorMessage,
    });
  }
);
