import { useQuery } from "@tanstack/react-query";
import { eventsByCommand } from "~/api/event/event.api";
import { useCommandsStore } from "~/store/commands/commands.store";
import { QueryKeys } from "~/utils/helpers/queryKeys";

export const useHomeCubit = () => {
  const { activeCommand } = useCommandsStore();

  const eventsAccessor = useQuery({
    queryKey: QueryKeys.EventsByCommand(activeCommand!),
    queryFn: () => eventsByCommand(activeCommand!),
  });

  return { eventsAccessor };
};
