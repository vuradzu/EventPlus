import axios from "axios";
import { getUrl } from "~/utils/helpers/apiFunctions";
import { AuthenticateRequest } from "./types/authenticateRequest";
import { CheckIfRegisteredRequest } from "./types/checkIfRegisteredRequest";
import { CheckIfRegisteredResult } from "./types/checkIfRegisteredResult";
import { JwtResult } from "./types/jwtResult";

const prefix = "jwt";
export const baseApiUrl =
  process.env.EXPO_PUBLIC_API_URL ?? "http://localhost:7257/";

const jwtApi = axios.create({
  baseURL: baseApiUrl,
  headers: {
    "Content-Type": "application/json",
  },
});

export const authenticate = async (
  request: Partial<AuthenticateRequest>
): Promise<JwtResult> => {
  const response = await jwtApi.post<JwtResult>(
    getUrl(prefix, "authenticate"),
    request
  );

  return response.data;
};

export const refresh = async (
  refreshToken: string,
  commandId?: number | null
): Promise<JwtResult> => {
  const response = await jwtApi.post<JwtResult>(getUrl(prefix, "refresh"), {
    refreshToken,
    commandId,
  });

  return response.data;
};

export const checkIfRegistered = async (request: CheckIfRegisteredRequest) => {
  const response = await jwtApi.get<CheckIfRegisteredResult>(
    getUrl(prefix, "check-if-registered", {
      key: request.key,
      provider: request.provider,
    })
  );

  return response.data;
};
