import { IRedeSocial } from './IRedeSocial';
import { IEvento } from './IEvento';

export interface IPalestrante {
    id: number;
    nome: string;
    miniCurriculo: string;
    imagemUrl: string;
    telefone: string;
    email: string;
    redesSociais: IRedeSocial[];
    palestranteEventos: IEvento[];

}
