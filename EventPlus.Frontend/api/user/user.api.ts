import { AxiosResponse } from "axios";
import FormData from "form-data";
import { getUrl } from "~/utils/helpers/apiFunctions";
import { _apiBase } from "..";
import { CheckIfUsernameAvailable } from "./types/checkIfUsernameAvailableResult";

const prefix = "user";

export const checkIfUsernameAvailable = async (
  username: string
): Promise<CheckIfUsernameAvailable> => {
  const response = await _apiBase.get<CheckIfUsernameAvailable>(
    getUrl(prefix, `check-username/${username}`)
  );

  return response.data;
};

type ImageFile = {
  uri: string;
  extension: string;
};
type ImageLink = string;

export const changeAvatar = async (
  image?: ImageFile | ImageLink | undefined
): Promise<string> => {
  const options: any = {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  };
  let requestUrl = getUrl(prefix, "avatar");
  let response: AxiosResponse<string, any>;

  if (image === undefined) {
    response = await _apiBase.post<string>(requestUrl);

    return response!.data;
  }

  if (typeof image === "string") {
    requestUrl += `?link=${image}`;
    response = await _apiBase.post<string>(requestUrl, options);

    return response!.data;
  }

  if (typeof image !== "string") {
    const formData = new FormData();

    formData.append("image", {
      uri: image.uri,
      type: `image/${image.extension}`,
      name: `image.${image.extension}`,
    });

    response = await _apiBase.post<string>(requestUrl, formData, options);

    return response!.data;
  }

  return response!.data;
};
