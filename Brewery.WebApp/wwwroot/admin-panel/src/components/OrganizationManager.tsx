import { useEffect, useState } from 'react'
import { Button, Table, Drawer } from 'antd'
import AddOrganizationForm from './AddOrganizationForm'
import organizationStore from '../stores/OrganizationStore'
import { observer } from 'mobx-react-lite'

const OrganizationManager = observer(() => {
  const [drawerVisible, setDrawerVisible] = useState(false)

  useEffect(() => {
    organizationStore.loadOrganizations()
  }, [])

  const columns = [
    {
      title: 'Наименование',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Адреса',
      dataIndex: 'addresses',
      key: 'addresses',
      render: (addresses: string[]) => addresses.join(', '),
    },
  ]

  return (
    <div>
      <Button type="primary" onClick={() => setDrawerVisible(true)} style={{ marginBottom: 16 }}>
        Управление организациями
      </Button>

      <Table
        dataSource={Array.isArray(organizationStore.organizations) ? organizationStore.organizations : []}
        columns={columns}
        rowKey="id"
        pagination={false}
      />

      <Drawer
        title="Добавить организацию"
        open={drawerVisible}
        onClose={() => setDrawerVisible(false)}
        width={480}
      >
        <AddOrganizationForm onSuccess={() => {
          setDrawerVisible(false)
          organizationStore.loadOrganizations()
        }} />
      </Drawer>
    </div>
  )
}, {})

export default OrganizationManager
