import { StringStream } from "../../u";
export declare enum StateType {
    Account = 64,
    Validator = 72
}
export interface StateDescriptorLike {
    type: string | number;
    key: string;
    field: string;
    value: string;
}
export declare class StateDescriptor {
    static deserialize(hex: string): StateDescriptor;
    static fromStream(ss: StringStream): StateDescriptor;
    /** Indicates the role of the transaction sender */
    type: StateType;
    /** The signing field of the transaction sender (scripthash for voting) */
    key: string;
    /** Indicates action for this descriptor */
    field: string;
    /** Data depending on field. For voting, this is the list of publickeys to vote for. */
    value: string;
    constructor(obj?: Partial<StateDescriptorLike>);
    readonly [Symbol.toStringTag]: string;
    serialize(): string;
    export(): StateDescriptorLike;
    equals(other: StateDescriptorLike): boolean;
}
export default StateDescriptor;
//# sourceMappingURL=StateDescriptor.d.ts.map