export const baseApiUrl =
  process.env.EXPO_PUBLIC_API_URL ?? "https://localhost:7257/";
export const CurrentCommandIdHeaderName = "Current-Command-Id-E+";

export const baseAxiosConfiguration = {
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
};
