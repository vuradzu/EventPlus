import { getUrl } from "~/utils/helpers/apiHelpers";
import { _apiBase } from "..";

const prefix = "test";

export const errorEndpoint = async () => {
  await _apiBase.get(getUrl(prefix, "error-endpoint"));
};
