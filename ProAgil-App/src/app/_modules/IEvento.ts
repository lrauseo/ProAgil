import { ILote } from './ILote';
import { IPalestrante } from './IPalestrante';
import { IRedeSocial } from './IRedeSocial';

export interface IEvento {
    id: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    email: string;
    imagemUrl: string;
    telefone: string;
    lotes: ILote[];
    redesSociais: IRedeSocial[];
    palestranteEventos: IPalestrante[];
}
