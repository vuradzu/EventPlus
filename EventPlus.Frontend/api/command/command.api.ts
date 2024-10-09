import { getUrl } from "~/utils/helpers/apiFunctions";
import { _apiBase } from "..";
import { CommandModel } from "./types/commandModel";
import { CreateCommandResult } from "./types/createCommandResult";
import { InviteResult } from "./types/inviteResult";
import { SwitchCommandResult } from "./types/switchCommandResult";

const prefix = "command";

export const _useInvite = async (code: string): Promise<InviteResult> => {
  const response = await _apiBase.post<InviteResult>(
    getUrl(prefix, `use-invite/${code}`)
  );

  return response.data;
};

export const _createCommand = async (
  name: string,
  description?: string
): Promise<CreateCommandResult> => {
  const response = await _apiBase.post<CreateCommandResult>(prefix, {
    name,
    description,
  });

  return response.data;
};

export const _getAllCommands = async (): Promise<CommandModel[]> => {
  const response = await _apiBase.get<CommandModel[]>(prefix);

  return response.data;
};

export const _switchCommand = async (
  commandId: number
): Promise<SwitchCommandResult> => {
  const response = await _apiBase.get<SwitchCommandResult>(
    getUrl(prefix, `switch-to/${commandId}`)
  );

  return response.data;
};
