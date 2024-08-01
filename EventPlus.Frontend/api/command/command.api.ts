import { getUrl } from "~/utils/helpers/apiFunctions";
import { _apiBase } from "..";
import { CreateCommandResult } from "./types/createCommandResult";
import { InviteResult } from "./types/inviteResult";

const prefix = "command";

export const useInvite = async (code: string): Promise<InviteResult> => {
  const response = await _apiBase.post<InviteResult>(
    getUrl(prefix, `use-invite/${code}`)
  );

  return response.data;
};

export const createCommand = async (
  name: string,
  description?: string
): Promise<CreateCommandResult> => {
  const response = await _apiBase.post<CreateCommandResult>(prefix, {
    name,
    description,
  });

  return response.data;
};
