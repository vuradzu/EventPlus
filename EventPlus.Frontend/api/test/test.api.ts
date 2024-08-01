import { getUrl } from "~/utils/helpers/apiFunctions";
import { _apiBase } from "..";

const prefix = "test";

export const errorEndpoint = async () => {
  await _apiBase.get(getUrl(prefix, "error-endpoint"));
};

export const authTest = async () => {
  await _apiBase.get(getUrl(prefix, "auth-test"));
};
