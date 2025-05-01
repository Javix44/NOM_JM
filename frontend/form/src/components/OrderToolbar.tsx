// /components/OrderToolbar.tsx
import { Button, Space } from 'antd';
import { LeftOutlined, RightOutlined, SearchOutlined, DeleteOutlined, PlusCircleFilled, CheckSquareFilled, FileOutlined, DoubleLeftOutlined, DoubleRightOutlined } from '@ant-design/icons';

type Props = {
  onPrev: () => void;
  onNext: () => void;
  onFirst: () => void;
  onDelete: () => void;
  onLast: () => void;
  onSearch: () => void;
  onGenerateReport: () => void;
  onNew: () => void;
  onSave: () => void;
  canPrev: boolean;
  canNext: boolean;
};

const OrderToolbar: React.FC<Props> = ({ onPrev, onNext, onDelete,
  onFirst, onLast, onSearch, canPrev, canNext, onGenerateReport,
  onNew, onSave }) => (

  <Space style={{ justifyContent: 'space-between', width: '100%' }}>
    <Space>
      <Button type="primary" className='btn-new'
       onClick={onNew}
      >New{<PlusCircleFilled />}</Button>
      <Button type="primary" className='btn-save'
      onClick={onSave}
      >Save{<CheckSquareFilled />}</Button>
      <Button type="primary" className='btn-delete'
        onClick={onDelete}
      >Delete{<DeleteOutlined />}</Button>
    </Space>
    <Space>
      <Button type="primary" className="btn-generate btn-new"
        onClick={onGenerateReport}
      ><FileOutlined /> Generate All Orders
      </Button>

      <Button icon={<SearchOutlined />} onClick={onSearch} />
      <Button icon={<DoubleLeftOutlined />} onClick={onFirst} disabled={!canPrev} />
      <Button icon={<LeftOutlined />} onClick={onPrev} disabled={!canPrev} />
      <Button icon={<RightOutlined />} onClick={onNext} disabled={!canNext} />
      <Button icon={<DoubleRightOutlined />} onClick={onLast} disabled={!canNext} />
    </Space>
  </Space>
);

export default OrderToolbar;
