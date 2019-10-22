import { Badge } from './badge.enum';

export interface IStat {
    name: string,
    battleCount: number,
    winCount: number,
    winRatio: number,
    winsToDesiredPercent: number,
    badge: Badge
}
