declare type RuleToken = {
    id: string;
    toString: (d: Date) => string;
};
export declare type FormatToken = string | RuleToken;
declare type ParseResult = {
    date: Date | null;
    missingPunctuation: string;
};
export declare function parse(str: string, tokens: FormatToken[], baseDate?: Date): ParseResult;
export declare function createFormat(s: string): FormatToken[];
export {};
