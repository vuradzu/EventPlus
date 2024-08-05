import { CommandModel } from "~/api/command/types/commandModel";

interface StoreData {
  commands: Omit<CommandModel, "events">[];
  activeCommand?: number | null;
}

interface StoreActions {
  addCommand: (command: CommandModel) => void;
  removeCommand: (commandId: number) => void;
  setActiveCommand: (id?: number | null) => void;
  clearStore: () => void;
}

export type CommandsStoreSchema = StoreData & StoreActions;
