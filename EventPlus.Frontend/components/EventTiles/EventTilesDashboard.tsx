import { useQueryClient, UseQueryResult } from "@tanstack/react-query";
import { useCallback, useMemo } from "react";
import { EventModelMini } from "~/api/event/types/eventModel";
import { GridView } from "~/components/GridView/GridView";
import { ClassNameProps } from "~/utils/helpers/classNames";
import { EventTile, EventTileSkeleton } from "./EventTile";

interface EventTilesDashboardProps {
  eventsAccessor: UseQueryResult<EventModelMini[], Error>;
}

export const EventTilesDashboard = (
  props: ClassNameProps<EventTilesDashboardProps, false>
) => {
  const { styles, eventsAccessor } = props;

  const eventsStatic: EventModelMini[] = [
    {
      id: 1,
      title: "Test 1",
      priority: "low",
      date: new Date(),
      assignmentsCount: 5,
      completedAssignmentsCount: 2,
      creatorId: 1,
      commandId: 1,
      created: new Date(),
    },
    {
      id: 2,
      title: "Test 2",
      priority: "medium",
      date: new Date(),
      assignmentsCount: 3,
      completedAssignmentsCount: 2,
      creatorId: 1,
      commandId: 1,
      created: new Date(),
    },
    {
      id: 3,
      title: "Test 3",
      priority: "high",
      date: new Date(),
      assignmentsCount: 110,
      completedAssignmentsCount: 6,
      creatorId: 1,
      commandId: 1,
      created: new Date(),
    },
    {
      id: 4,
      title: "Test 4",
      priority: "low",
      date: new Date(),
      assignmentsCount: 11,
      completedAssignmentsCount: 3,
      creatorId: 1,
      commandId: 1,
      created: new Date(),
    },
    {
      id: 5,
      title: "Test 5",
      priority: "low",
      date: new Date(),
      assignmentsCount: 0,
      completedAssignmentsCount: 3,
      creatorId: 1,
      commandId: 1,
      created: new Date(),
    },
  ];

  return (
    <GridView
      styles={styles}
      items={eventsAccessor.data}
      renderItem={(item) => <EventTile event={item} />}
      loadingRenderItem={() => <EventTileSkeleton/>}
      loadingItemsCount={6}
      onRefresh={async () => {
        await eventsAccessor.refetch();
      }}
    />
  );
};
