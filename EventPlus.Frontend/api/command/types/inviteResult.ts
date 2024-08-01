import { CommandTokensModel } from "./commandTokensModel";

interface InviteSuccessResult {
  isSuccess: true;
  commandId: number;
  tokens: CommandTokensModel;
}
interface InviteFailureResult {
  isSuccess: false;
  message: string;
}

export type InviteResult = InviteSuccessResult | InviteFailureResult;
