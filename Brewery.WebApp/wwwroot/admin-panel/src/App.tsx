import { Layout, Menu } from 'antd'
import { Link, Route, Routes } from 'react-router-dom'
import OrganizationsPage from "./pages/OrganizationsPage.tsx";

const { Header, Content } = Layout

function App() {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header>
        <Menu theme="dark" mode="horizontal">
          <Menu.Item key="orgs">
            <Link to="/organizations">Управление организациями</Link>
          </Menu.Item>
        </Menu>
      </Header>
      <Content style={{ padding: '24px' }}>
        <Routes>
          <Route path="/organizations" element={<OrganizationsPage /> as React.ReactNode} />
        </Routes>
      </Content>
    </Layout>
  )
}

export default App