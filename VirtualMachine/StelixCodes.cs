namespace VirtualMachine
{
    public enum StelixCodes : byte
    {
        EX,
        MODEL,
        END_MODEL,
        MARKER,
        CR_SECTION,
        END_SECTION,
        CR_FUNC,
        END_FUNC,
        CR_VAR,
        VAR_SET,
        CALL_FUNC,
        CALL_VAR,
        
        INSTRUCTION_START_ID, /* if a id is bigger than this one's id it will be a instruction */
        INSTRUCTION_DEBUG,
        INSTRUCTION_CALL_FUNCTION,


    }
}