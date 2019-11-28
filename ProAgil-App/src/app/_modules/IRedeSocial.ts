import { IPalestrante } from './IPalestrante';
export interface IRedeSocial {
    id: number;
    nome: string;
    url: string;
    eventiId?: number;
    palestranteId?: number;
    palestrante: IPalestrante;
}
