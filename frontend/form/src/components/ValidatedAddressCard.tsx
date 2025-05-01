// /components/ValidatedAddressCard.tsx
import { Card, Col, Divider, Input, Row, Typography } from 'antd';

const ValidatedAddressCard = () => {
  const fields = ['Street', 'City', 'State', 'Postal Code', 'Country', 'Geocoded Coordinates'];

  return (
    <div>
      <Typography.Title level={2}>Validated Address</Typography.Title>
      <Row gutter={16}>
        {fields.map(field => (
          <Col span={8} key={field}>
            <label
              style={{ fontSize: '18px', fontWeight: 'bold' }}
            >{field}</label>
            <Input placeholder={field}
              className='custom-disabled'
              disabled />
          </Col>
        ))}
      </Row>
      <Divider />
      <Card className="map-placeholder" bodyStyle={{ padding: 5 }}>
        <iframe
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3876.511342802469!2d-89.5611648!3d13.9792887!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zMTPCsDU4JzQ1LjQiTiA4OcKwMzMnNDAuMiJX!5e0!3m2!1ses!2ssv!4v1680000000000"
          loading="lazy"
          referrerPolicy="no-referrer-when-downgrade"
        ></iframe>
      </Card>
    </div>
  );
};

export default ValidatedAddressCard;
