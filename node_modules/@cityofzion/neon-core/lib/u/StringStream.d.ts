/**
 * A simple string stream that allows user to read a string byte by byte using read().
 * @param str - The string to read as a stream.
 */
export declare class StringStream {
    str: string;
    pter: number;
    /**
     * Initializes the stream with given string and pointer at position 0.
     */
    constructor(str?: string);
    /**
     * Checks if reached the end of the stream. Does not mean stream is actually empty (this.str is not empty).
     * @example
     * const ss = new StringStream("01020304");
     * ss.isEmpty(); // false
     * ss.pter = 3;
     * ss.isEmpty(); // true
     */
    isEmpty(): boolean;
    /**
     * Peek at the next bytes on the string. May return less than intended bytes if reaching end of stream.
     * @example
     * const ss = new StringStream("0102");
     * ss.peek();  // "01"
     * ss.peek(5); // "0102"
     */
    peek(bytes?: number): string;
    /**
     * Reads some bytes off the stream.
     * @param bytes Number of bytes to read
     * @example
     * const ss = new StringStream("01020304");
     * ss.read(); // "01"
     * ss.read(2); // "0203"
     */
    read(bytes?: number): string;
    /**
     * Reads some bytes off the stream.
     * A variable-length integer is first read off the stream and then bytes equal to the integer is read off and returned.
     */
    readVarBytes(): string;
    /**
     * Reads an integer of variable bytelength. May consume up to 9 bytes.
     * The first byte read indicates if more bytes need to be read off.
     */
    readVarInt(): number;
    /**
     * Resets the pointer to start of string.
     * @example
     * const ss = new StringStream("010203");
     * ss.read(); //"01"
     * ss.reset();
     * ss.read(); // "01"
     */
    reset(): void;
    /**
     * Returns a printable string of the characters around the pointer.
     * Used for debugging.
     */
    context(): string;
}
export default StringStream;
//# sourceMappingURL=StringStream.d.ts.map