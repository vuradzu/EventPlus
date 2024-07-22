export const imageToBlob = async (url: string) =>
  await (await fetch(url)).blob();
