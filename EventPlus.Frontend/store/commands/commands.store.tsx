import { useStore } from "zustand";
import { CommandModel } from "~/api/command/types/commandModel";
import { EventModelMini } from "~/api/event/types/eventModel";
import { createStore } from "~/utils/helpers/storeHelpers";
import { CommandsStoreSchema } from "./commands.schema";

export const commandsStore = createStore<CommandsStoreSchema>(
  "Commands Store",
  (set) => {
    const addCommand = (command: CommandModel) => {
      set((state) => {
        if (!!state.commands.find((c) => c.id === command.id)) return;

        const newCommand: CommandModel = {
          id: command.id,
          name: command.name,
          description: command.description,
          avatar: command.avatar,
          creatorId: command.creatorId,
          created: command.created,
          events: command.events,
        };

        state.commands = [...state.commands, newCommand];
      });
    };

    const addCommandEvent = (event: EventModelMini) => {
      set((state) => {
        state.commands = [
          ...state.commands.map((c) => ({
            ...c,
            events:
              c.id === state.activeCommand ? [...c.events, event] : c.events,
          })),
        ];
      });
    };

    const removeCommand = (commandId: number) => {
      set((state) => {
        state.commands = [...state.commands.filter((c) => c.id !== commandId)];
      });
    };

    const setActiveCommand = (id?: number | null) => {
      set((state) => {
        state.activeCommand = id;
      });
    };

    const clearStore = () => {
      set((state) => {
        state.activeCommand = null;
        state.commands = [];
      });
    };

    return {
      commands: [],
      activeCommand: null,
      addCommand,
      addCommandEvent,
      removeCommand,
      setActiveCommand,
      clearStore,
    };
  },
  { persisted: true }
);

export const useCommandsStore = () => useStore(commandsStore);
