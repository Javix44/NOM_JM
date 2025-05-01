import { Select } from 'antd';
import { Employee } from '../service/types';
import { DefaultOptionType } from 'antd/es/select';

type Props = {
  employees: Employee[];
  selectedEmployee: number | null;
  onChange: (value: number) => void;
};

const EmployeeSelect = ({ employees, selectedEmployee, onChange }: Props) => {
  
  // Función segura para filtrar opciones
  const filterOption = (input: string, option?: DefaultOptionType) => {
    if (option?.label) {
      return (option.label as string).toLowerCase().includes(input.toLowerCase());
    }
    return false;
  };

  return (
    <>
      <label style={{ fontSize: '18px', fontWeight: 'bold' }}>Employee</label>
      <Select
        placeholder="Select Employee"
        className="custom-placeholder"
        value={selectedEmployee !== null ? selectedEmployee : undefined}
        onChange={onChange}
        showSearch
        filterOption={filterOption}
        options={employees.map(e => ({
          label: `${e.firstName} ${e.lastName}`,
          value: e.employeeId,
        }))}
      />
    </>
  );
};

export default EmployeeSelect;
