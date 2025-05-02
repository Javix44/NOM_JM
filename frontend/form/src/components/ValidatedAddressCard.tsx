import { Spin, Card, Col, Divider, Input, Row, Typography } from 'antd';
import React from 'react';

interface Props {
  validatedData: {
    formattedAddress: string;
    street: string;
    city: string;
    state: string;
    postalCode: string;
    country: string;
    latitude: number;
    longitude: number;
  } | null;
  maploading: boolean;
}

const ValidatedAddressCard: React.FC<Props> = ({ validatedData, maploading }) => {
  return (
    <div>
      <Typography.Title level={2}>Validated Address</Typography.Title>
      <Row gutter={16}>
        <Col span={8}>
          <label>Street</label>
          <Input
            className='custom-disabled'
            value={validatedData?.street || ''} disabled />
        </Col>
        <Col span={8}>
          <label>City</label>
          <Input
            className='custom-disabled'

            value={validatedData?.city || ''} disabled />
        </Col>
        <Col span={8}>
          <label>State</label>
          <Input
            className='custom-disabled'

            value={validatedData?.state || ''} disabled />
        </Col>
        <Col span={8}>
          <label>Postal Code</label>
          <Input
            className='custom-disabled'

            value={validatedData?.postalCode || ''} disabled />
        </Col>
        <Col span={8}>
          <label>Country</label>
          <Input
            className='custom-disabled'

            value={validatedData?.country || ''} disabled />
        </Col>
        <Col span={8}>
          <label>Geocoded Coordinates</label>
          <Input
            className='custom-disabled'
            value={
              validatedData
                ? `${validatedData.latitude}, ${validatedData.longitude}`
                : ''
            }
            disabled
          />
        </Col>
      </Row>

      <Divider />
      <Card className="map-placeholder" bodyStyle={{ padding: 5 }}>
        <Spin spinning={maploading}
          tip="Loading map...">
          {validatedData && (
            <iframe
              key={`${validatedData.latitude}-${validatedData.longitude}`} // Fuerza rerender
              width="100%"
              height="300"
              loading="lazy"
              src={`https://maps.google.com/maps?q=${validatedData.latitude},${validatedData.longitude}&z=15&output=embed`}
              referrerPolicy="no-referrer-when-downgrade"
              style={{ border: 0 }}
            ></iframe>
          )}
        </Spin>
      </Card>

    </div>
  );
};

export default ValidatedAddressCard;
