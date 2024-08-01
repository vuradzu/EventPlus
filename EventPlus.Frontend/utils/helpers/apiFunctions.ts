export const getUrl = (
    basePrefix: string,
    path: string,
    queryParams?: Record<string, any>
  ) => {
    let resultUrl = `${basePrefix}/${path}`;
  
    if (queryParams) {
      const queryParamsEntries = Object.entries(queryParams);
  
      resultUrl += `?${queryParamsEntries[0][0]}=${queryParamsEntries[0][1]}`;
  
      if (queryParamsEntries.length > 1) {
        queryParamsEntries.shift();
  
        queryParamsEntries.forEach((queryParamEntry) => {
          resultUrl += `&${queryParamEntry[0]}=${queryParamEntry[1]}`;
        });
      }
    }
    return resultUrl;
  };