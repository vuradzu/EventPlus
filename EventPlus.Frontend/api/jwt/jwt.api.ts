import { getUrl } from "~/utils/helpers/apiHelpers";
import { _apiBase } from "..";
import { AuthenticateRequest } from "./types/authenticateRequest";
import { CheckIfRegisteredRequest } from "./types/checkIfRegisteredRequest";
import { CheckIfRegisteredResult } from "./types/checkIfRegisteredResult";
import { JwtResult } from "./types/jwtResult";

const prefix = "jwt";

export const authenticate = async (
  request: Partial<AuthenticateRequest>
): Promise<JwtResult> => {
  const response = await _apiBase.post<JwtResult>(
    getUrl(prefix, "authenticate"),
    request
  );

  return response.data;
};

export const checkIfRegistered = async (request: CheckIfRegisteredRequest) => {
  const response = await _apiBase.get<CheckIfRegisteredResult>(
    getUrl(prefix, "check-if-registered", {
      key: request.key,
      provider: request.provider,
    })
  );

  return response.data;
};
