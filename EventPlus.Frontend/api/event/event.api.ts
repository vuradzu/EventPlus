import { getUrl } from "~/utils/helpers/apiFunctions";
import { _apiBase } from "..";
import { EventModelMini } from "./types/eventModel";

const prefix = "event";

export const eventsByCommand = async (
  commandId: number
): Promise<EventModelMini[]> => {
  const response = await _apiBase.post<EventModelMini[]>(
    getUrl(prefix, `by-command/${commandId}`)
  );

  return response.data;
};
