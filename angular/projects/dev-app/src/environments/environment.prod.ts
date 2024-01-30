import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'GeDatasus',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44318/',
    redirectUri: baseUrl,
    clientId: 'GeDatasus_App',
    responseType: 'code',
    scope: 'offline_access GeDatasus',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44318',
      rootNamespace: 'NecnatAbp.Br.GeDatasus',
    },
    GeDatasus: {
      url: 'https://localhost:44327',
      rootNamespace: 'NecnatAbp.Br.GeDatasus',
    },
  },
} as Environment;
