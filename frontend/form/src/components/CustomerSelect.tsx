import { Select } from 'antd';
import { Customer } from '../service/types';
import { DefaultOptionType } from 'antd/es/select';

type Props = {
  customers: Customer[];
  selectedCustomer: string;
  onChange: (value: string) => void;
};

const CustomerSelect = ({ customers, selectedCustomer, onChange }: Props) => {
  
  const filterOption = (input: string, option?: DefaultOptionType) => {
    if (option?.label) {
      return (option.label as string).toLowerCase().includes(input.toLowerCase());
    }
    return false;
  };

  return (
    <>
      <label style={{ fontSize: '18px', fontWeight: 'bold' }}>Customer</label>
      <Select
        placeholder="Select Customer"
        className="custom-placeholder"
        value={selectedCustomer !== '' ? selectedCustomer : undefined}
        onChange={onChange}
        showSearch
        filterOption={filterOption}
        options={customers.map(cust => ({
          label: cust.companyName,
          value: cust.customerId,
        }))}
      />
    </>
  );
};

export default CustomerSelect;
