import { AxiosError, AxiosResponse } from "axios";
import Toast from "react-native-toast-message";

export const errorInterceptorFulfilled = async (
    config: AxiosResponse<any, any>
  ) => {
    return config;
  };
  
  export const errorInterceptorError = (error: AxiosError) => {
    const data = error.response?.data as any;
  
    const errorMessage = data?.errors?.[0] ?? "Помилка сервера";
  
    Toast.show({
      type: "error",
      text1: "Помилка :(",
      text2: errorMessage,
    });
  };