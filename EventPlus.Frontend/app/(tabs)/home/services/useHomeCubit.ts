import { useQuery } from "@tanstack/react-query"
import { eventsByCommand } from "~/api/event/event.api"
import { QueryKeys } from "~/utils/helpers/queryKeys"

export const useHomeCubit = () => {
    const commandId = 1;

    const eventsAccessor = useQuery({
        queryKey: QueryKeys.EventsByCommand(commandId),
        queryFn: () => eventsByCommand(commandId),
    })
}