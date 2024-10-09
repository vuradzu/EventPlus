import { UseQueryResult } from "@tanstack/react-query";
import { EventModelMini } from "~/api/event/types/eventModel";
import { GridView } from "~/components/GridView/GridView";
import { useCommandsStore } from "~/store/commands/commands.store";
import { ClassNameProps } from "~/utils/helpers/classNames";
import { EventTile, EventTileSkeleton } from "./EventTile";

interface EventTilesDashboardProps {
  eventsAccessor: UseQueryResult<EventModelMini[], Error>;
}

export const EventTilesDashboard = (
  props: ClassNameProps<EventTilesDashboardProps, false>
) => {
  const { styles, eventsAccessor } = props;
  const { commandLoading } = useCommandsStore();

  return (
    <GridView
      styles={styles}
      items={eventsAccessor.data}
      isLoading={eventsAccessor.isFetching || commandLoading}
      renderItem={(item) => <EventTile event={item} />}
      loadingRenderItem={() => <EventTileSkeleton />}
      loadingItemsCount={6}
      onRefresh={async () => {
        await eventsAccessor.refetch();
      }}
    />
  );
};
