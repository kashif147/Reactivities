import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { Item, Button,  Segment, Icon } from "semantic-ui-react";
import { IActivity } from "../../../app/models/activity";
import ActivityStore from "../../../app/stores/activityStore";

export const ActivityListItem: React.FC<{ activity: IActivity }> = ({
  activity,
}) => {
  const activityStore = useContext(ActivityStore);
  const { deleteActivity, submitting, target } = activityStore;
  return (
    <Segment.Group>
      <Segment>
        <Item.Group>
          <Item>
            <Item.Image size="tiny" circular src="/assets/user.png" />
            <Item.Content>
              <Item.Header as="a">{activity.title}</Item.Header>

              <Item.Description>Hosted by Shahnaz Kashif</Item.Description>
            </Item.Content>
          </Item>
        </Item.Group>
      </Segment>
      <Segment>
        <Icon name="clock" />
        {activity.date}
        <Icon name="marker" /> {activity.venue},{activity.city}
      </Segment>
      <Segment secondary>Attendees wil go there</Segment>
      <Segment clearing>
        <span>{activity.description}</span>
        <Button
          as={Link}
          to={`/activities/${activity.id}`}
          floated="right"
          content="View"
          color="blue"
        />
      </Segment>
    </Segment.Group>
  );
};
