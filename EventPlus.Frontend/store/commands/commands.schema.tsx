import { CommandModel } from "~/api/command/types/commandModel";
import { EventModelMini } from "~/api/event/types/eventModel";

interface StoreData {
  commands: CommandModel[];
  activeCommand?: number | null;
}

interface StoreActions {
  addCommand: (command: CommandModel) => void;
  addCommandEvent: (event: EventModelMini) => void;
  removeCommand: (commandId: number) => void;
  setActiveCommand: (id?: number | null) => void;
  clearStore: () => void;
}

export type CommandsStoreSchema = StoreData & StoreActions;
