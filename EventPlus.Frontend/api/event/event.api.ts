import { commandsStore } from "~/store/commands/commands.store";
import { getUrl } from "~/utils/helpers/apiFunctions";
import { _apiBase } from "..";
import { CreateEventRequest } from "./types/createEventRequest";
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

export const createEvent = async (
  request: CreateEventRequest
): Promise<EventModelMini> => {
  const commandId = commandsStore.getState().activeCommand;

  const response = await _apiBase.post<EventModelMini>(
    getUrl(prefix, `${commandId}`),
    request
  );

  return response.data;
};
