import React from 'react';
import { Button, Container, Menu } from 'semantic-ui-react';

interface IProps {
        OpenCreateForm: () => void;
}
export const NavBar: React.FC<IProps> = ({OpenCreateForm}) => {
    return (
        <Menu fixed='top' inverted>
            <Container>
                <Menu.Item >
                        <img src="/assets/logo.png" alt="logo" style={{marginRight: '10px'}}></img>
                        Reactivities
                </Menu.Item>
                <Menu.Item name='Activities' />
                <Menu.Item>
                    <Button onClick={OpenCreateForm} positive content='Create Activity'></Button>
                </Menu.Item>

            </Container>
      </Menu>
    )
}
