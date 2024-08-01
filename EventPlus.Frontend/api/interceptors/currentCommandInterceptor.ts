import { InternalAxiosRequestConfig } from "axios";
import Toast from "react-native-toast-message";
import { userStore } from "~/store/user/user.store";
import { CurrentCommandIdHeaderName } from "../_base";

export const currentCommandInterceptor = (
  request: InternalAxiosRequestConfig<any>
) => {
  const currentCommandId = userStore.getState().activeCommand;

  Toast.show({
    type: "info",
    text1: "Id команди",
    text2: currentCommandId?.toString(),
  });

  if (!currentCommandId) return request;

  request.headers.set(CurrentCommandIdHeaderName, currentCommandId);

  return request;
};
