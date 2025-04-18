import { useState } from 'react'
import { Button, Form, Input, Space } from 'antd'
import axios from 'axios'

const AddOrganizationForm = ({ onSuccess }: { onSuccess: () => void }) => {
  const [form] = Form.useForm()
  const [addresses, setAddresses] = useState([''])

  const handleAddAddress = () => {
    setAddresses([...addresses, ''])
  }

  const handleAddressChange = (index: number, value: string) => {
    const newAddresses = [...addresses]
    newAddresses[index] = value
    setAddresses(newAddresses)
  }

  const handleFinish = async (values: any) => {
    axios.defaults.baseURL = 'http://localhost:5029';
    
    await axios.post('/api/organizations', {
      name: values.name,
      addresses
    })
    form.resetFields()
    setAddresses([''])
    onSuccess()
  }

  return (
    <Form form={form} onFinish={handleFinish} layout="vertical">
      <Form.Item name="name" label="Название организации" rules={[{ required: true }]}>
        <Input />
      </Form.Item>
      <div>
        <label>Адреса магазинов:</label>
        {addresses.map((addr, index) => (
          <Input
            key={index}
            value={addr}
            onChange={(e) => handleAddressChange(index, e.target.value)}
            style={{ marginBottom: 8 }}
          />
        ))}
        <Button onClick={handleAddAddress} type="dashed" block>
          Добавить адрес
        </Button>
      </div>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          Сохранить
        </Button>
      </Form.Item>
    </Form>
  )
}

export default AddOrganizationForm
