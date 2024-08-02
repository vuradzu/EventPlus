import { CommandModel } from "./commandModel";
import { CommandTokensModel } from "./commandTokensModel";

interface InviteSuccessResult {
  isSuccess: true;
  command: CommandModel;
  tokens: CommandTokensModel;
}
interface InviteFailureResult {
  isSuccess: false;
  message: string;
}

export type InviteResult = InviteSuccessResult | InviteFailureResult;
