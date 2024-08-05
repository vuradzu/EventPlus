import React from "react";
import { EventModelMini } from "~/api/event/types/eventModel";
import { GridView } from "~/components/GridView/GridView";
import { ClassNameProps } from "~/utils/helpers/classNames";
import { EventTile } from "./EventTile";

interface EventTilesDashboardProps {
  events: EventModelMini[]
}

export const EventTilesDashboard = (
  props: ClassNameProps<EventTilesDashboardProps, false>
) => {
  const { styles, events } = props;

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
      assignmentsCount: 6,
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
  ];

  return (
    <GridView
      styles={styles}
      items={eventsStatic}
      renderItem={(event) => <EventTile event={event} />}
    />
  );
};
