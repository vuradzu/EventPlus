import React from "react";
import { Priority } from "~/api/types/Priority";
import { ClassNameProps } from "~/utils/helpers/classNames";
import { Tab, Tabs } from "../Tabs/Tabs";

interface PriorityTabsProps {
  priority?: Priority;
  onPriorityChange?: (id: Priority) => void;
}

const PriorityTabs = (props: ClassNameProps<PriorityTabsProps, false>) => {
  const { styles, priority, onPriorityChange } = props;

  const priorityTabs: Tab[] = [
    { id: "low", title: "Низький" },
    { id: "medium", title: "Середній" },
    { id: "high", title: "Високий" },
  ];

  return (
    <Tabs
      styles={styles}
      tabs={priorityTabs}
      selectedTab={priority}
      onTabChange={(tab) =>
        !!onPriorityChange && onPriorityChange(tab as Priority)
      }
    />
  );
};

export default PriorityTabs;
