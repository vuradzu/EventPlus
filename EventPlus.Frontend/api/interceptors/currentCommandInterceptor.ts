import { InternalAxiosRequestConfig } from "axios";
import { commandsStore } from "~/store/commands/commands.store";
import { CurrentCommandIdHeaderName } from "../_base";

export const currentCommandInterceptor = (
  request: InternalAxiosRequestConfig<any>
) => {
  const currentCommandId = commandsStore.getState().activeCommand;

  if (!currentCommandId) return request;

  request.headers.set(CurrentCommandIdHeaderName, currentCommandId);

  return request;
};
