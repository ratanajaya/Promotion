import React, { useEffect, useState } from 'react';
import { Col, Layout, Menu, Row, Form, Input, Select, Upload, Button, Checkbox, DatePicker } from 'antd';
import {  UploadOutlined, UserOutlined, VideoCameraOutlined  } from '@ant-design/icons';
import moment from 'moment';

const { Header, Content, Footer, Sider } = Layout;
const { Option } = Select;
const { RangePicker } = DatePicker;

const apiBase = 'https://localhost:7274/';
const axios = require('axios').default;

const defaultPromo = {
  promotionId: '',
  promoType: 'S',
  valueType: 'P',
  value: 0,
  description: '',
  startDate: null,
  endDate: null,
  item: '',
  storeIds: []
};

export default function MainPage() {
  const [stores, setStores] = useState([]);
  const [promotion, setPromotion] = useState(defaultPromo);

  useEffect(() => {
    axios.get(`${apiBase}Main/GetStores`)
      .then((response) => {
        setStores(response.data);
      })
      .catch((error) => {
      });
  },[]);

  function handleChange(name, val){
    debugger
    if(name === 'date'){
      setPromotion(prev => {
        prev.startDate = val[0]._d.toISOString();
        prev.endDate = val[1]._d.toISOString();
        return { ...prev };
      });
    }
    else if(name === 'value'){
      setPromotion(prev => {
        const max = prev.valueType === 'Percentage' ? 100 : 999999999;
        const clampedVal = Math.min(Math.max(val, 0), max);

        prev.value = clampedVal;
        return { ...prev };
      });
    }
    else{
      setPromotion(prev => {
        prev[name] = val;
        return { ...prev };
      });
    }
  }

  function handleChkChange(id, checked){
    setPromotion(prev => {
      if(id === 'ALL'){
        prev.storeIds = checked ? stores.map(a => a.storeId) : [];
      }
      else if(checked && !prev.storeIds.includes(id)){
        prev.storeIds = [...prev.storeIds, id];
      }
      else{
        prev.storeId = prev.storeIds.filter(a => a !== id);
      }
      
      return { ...prev };
    });
  }

  function handleSubmit(){
    //console.log(promotion);
    axios.post(`${apiBase}Main/SubmitPromotion`, promotion)
      .then((response) => {
        setPromotion(prev => {
          prev.promotionId = response.data;
          return { ...prev };
        });
      })
      .catch((error) => {
      });
  }

  function handleClear(){
    console.log('def', defaultPromo);
    setPromotion({...defaultPromo});
  }

  const upload = {
    name: 'file',
    action: 'https://www.mocky.io/v2/5cc8019d300000980a055e76',
    headers: {
      authorization: 'authorization-text',
    },
    onChange(info) {
      if (info.file.status !== 'uploading') {
        console.log(info.file, info.fileList);
      }
      if (info.file.status === 'done') {
        //message.success(`${info.file.name} file uploaded successfully`);
      } else if (info.file.status === 'error') {
        //message.error(`${info.file.name} file upload failed.`);
      }
    },
  };

  return (
    <Layout>
      <Sider
        breakpoint="lg"
        collapsedWidth="0"
        onBreakpoint={broken => {
          console.log(broken);
        }}
        onCollapse={(collapsed, type) => {
          console.log(collapsed, type);
        }}
      >
        <div className="logo" />
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={['4']}
          items={[
            {key:'1', icon: React.createElement(UserOutlined), label:'HOME' },
            {key:'2', icon: React.createElement(VideoCameraOutlined), label:'PROMOTION' },
          ]}
        />
      </Sider>
      <Layout>
        <Header className="site-layout-sub-header-background" style={{ padding: 0 }} />
        <Content style={{ margin: '24px 16px 0', maxWidth:'600px', minHeight: '100vh' }}>
          <div className="site-layout-background" style={{ padding: 24, display: 'flex', gap: '10px' }}>
            <div style={{flex:'1', textAlign:'left'}}>
              <ItemLayout label={"Promo ID"}>
                <Input readonly={true} value={promotion.promotionId}></Input>
              </ItemLayout>

              <ItemLayout label={"Promo Type"}>
                <Select value={[promotion.promoType]} onChange={(val) => handleChange('promoType', val) } style={{width:'100%'}}>
                  <Option value="S">Simple Discount</Option>
                  <Option value="C">Completed Discount</Option>
                </Select>
              </ItemLayout>

              <ItemLayout label={"Value Type"}>
                <div style={{display: 'flex', gap:'10px'}}>
                  <div style={{flex:'3'}}>
                    <Select value={[promotion.valueType]} onChange={(val) => handleChange('valueType', val) } style={{width:'100%'}}>
                      <Option value="P">Percentage</Option>
                      <Option value="A">Amount</Option>
                    </Select>
                  </div>
                  <div style={{flex:'2'}}>
                    <Input type={"number"} value={promotion.value} onChange={(e) => handleChange('value', e.target.value) }></Input>
                  </div>
                </div>
              </ItemLayout>

              <ItemLayout label={"Item"}>
                <div style={{display: 'flex', gap:'10px'}}>
                  <div style={{flex:'3'}}>
                    <Input readOnly={true}></Input>
                  </div>
                  <div style={{flex:'2'}}>
                    <Upload {...upload}>
                      <Button icon={<UploadOutlined />}>Click to Upload</Button>
                    </Upload>
                  </div>
                </div>
              </ItemLayout>

              <ItemLayout label={"Store"}>
                <table>
                  <tbody>
                  <tr>
                    <td><Checkbox checked={JSON.stringify(stores.map(s => s.storeId).sort()) === JSON.stringify(promotion.storeIds.sort())} onChange={(e) => handleChkChange('ALL', e.target.checked)} /></td>
                    <td>ALL</td>
                    <td></td>
                  </tr>
                  {stores.map((s, index) => {
                    return(<tr key={index}>
                      <td><Checkbox checked={promotion.storeIds.includes(s.storeId)} onChange={(e) => handleChkChange(s.storeId, e.target.checked)} /></td>
                      <td>{s.storeId}</td>
                      <td>{s.name}</td>
                    </tr>);
                  })}
                  </tbody>
                </table>
              </ItemLayout>
            </div>
            <div style={{flex:'1', textAlign:'left'}}>
              <ItemLayout label={"Promo Description"}>
                <Input value={promotion.description} onChange={(e) => handleChange('description', e.target.value) }></Input>
              </ItemLayout>
              <ItemLayout label={"Promo Duration"}>
                <RangePicker value={ promotion.startDate === null ? null : [moment(promotion.startDate), moment(promotion.endDate)]} style={{width:'100%'}} onChange={(val) => handleChange('date', val) } />
              </ItemLayout>
            </div>
          </div>
          <div>
            <Button type='primary' onClick={handleSubmit}>Submit</Button>
            <Button type='primary' danger={true} onClick={handleClear}>Clear</Button>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
}

function ItemLayout(props){
  return(
    <div>
      <div style={{fontWeight:'bold'}}>{props.label}</div>
      <div>{props.children}</div>
    </div>
  );
}