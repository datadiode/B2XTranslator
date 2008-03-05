﻿/*
 * Copyright (c) 2008, DIaLOGIKa
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *        notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of DIaLOGIKa nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY DIaLOGIKa ''AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL DIaLOGIKa BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Text;
using DIaLOGIKa.b2xtranslator.CommonTranslatorLib;
using DIaLOGIKa.b2xtranslator.StructuredStorageReader;

namespace DIaLOGIKa.b2xtranslator.DocFileFormat
{
    public class ParagraphProperties : IVisitable
    {
        public enum JustificationCode
        {
            left = 0,
            center,
            right,
            both,
            distribute,
            mediumKashida,
            numTab,
            highKashida,
            lowKashida,
            thaiDistribute,
        }

        /// <summary>
        /// Index the style descriptor. This is an index to an STD in the STSH structure
        /// </summary>
        public UInt16 istd;

        /// <summary>
        /// When 1, paragraph is a side by side paragraph
        /// </summary>
        public bool fSideBySide;

        /// <summary>
        /// Keep entire paragraph on one page if possible
        /// </summary>
        public bool fKeep;

        /// <summary>
        /// Keep paragraph on same page with next paragraph if possible
        /// </summary>
        public bool fKeepFollow;

        /// <summary>
        /// Start this paragraph on new page
        /// </summary>
        public bool fPageBreakBefore;

        /// <summary>
        /// Border line style<br/>
        /// 0 single<br/>
        /// 1 thick<br/>
        /// 2 double<br/>
        /// 3 shadow<br/>
        /// </summary>
        public byte brcl;

        /// <summary>
        /// Rectangle boder codes<br/>
        /// 0 none<br/>
        /// 1 border above<br/>
        /// 2 border below<br/>
        /// 15 box around<br/>
        /// 16 bar to left of paragraph
        /// </summary>
        public byte brcp;

        /// <summary>
        /// When non-zero, list level for this paragraph
        /// </summary>
        public byte ilvl;

        /// <summary>
        /// When non-zero, (1-based) index intothe pllfo identifiying the 
        /// list to which the paragraph belongs
        /// </summary>
        public byte ilfo;

        /// <summary>
        /// No line numbering for this paragraph.
        /// </summary>
        public bool fNoLynn;

        /// <summary>
        /// Space before paragraph
        /// </summary>
        public UInt32 dyaBefore;

        /// <summary>
        /// Space after paragraph
        /// </summary>
        public UInt32 dyaAfter;

        /// <summary>
        /// Paragraph is in table (archaic)
        /// </summary>
        public bool fInTableW97;

        /// <summary>
        /// Table trailer paragraph (last paragraph in table row)
        /// </summary>
        public bool fTtp;

        /// <summary>
        /// When positive, is the horizontal distance from the 
        /// reference frame specified bypap.pcHorz.<br/>
        /// 0 means paragraph is positioned at the left with respect to the 
        /// reference frame specified by pcHorz.<br/>
        /// Certain negative values have special meaning:<br/>
        /// -4 paragraph centered horizontal within reference frame<br/>
        /// -8 paragraph adjusted right within reference frame<br/>
        /// -12 paragraph placed immediately inside of reference frame<br/>
        /// -16 paragraph placed immediately outside of reference frame
        /// </summary>
        public Int32 dxaAbs;

        /// <summary>
        /// When positive, is the vertical distance from the reference 
        /// frame specified by pcVert.<br/>
        /// 0 means paragraph's y-position is unconstrained. 
        /// Certain negative values have special meaning:<br/>
        /// -4 paragraph is placed at top of reference frame<br/>
        /// -8 paragraph is centered vertically within reference frame<br/>
        /// -12 paragraph is placed at bottom of reference frame
        /// </summary>
        public Int32 dyaAbs;

        /// <summary>
        /// When not 0, paragraph is constrained to be dxaWidth wide, 
        /// independent of current margin or column settings.
        /// </summary>
        public Int32 dxaWidth;

        /// <summary>
        /// 
        /// </summary>
        public bool fBrLnAbove;

        /// <summary>
        /// 
        /// </summary>
        public bool fBrLnBelow;

        /// <summary>
        /// Vertical position code. Specifies coodrinate frame to use when 
        /// paragraphs are absolutely positioned.<br/>
        /// 0 vertical position coordinates are relative to margin<br/>
        /// 1 coordinates are relative to page<br/>
        /// 2 coordinates are relative to text. This means: relative to where 
        /// the nex non-APO text would have been placed if this APO did not exist
        /// </summary>
        public byte pcVert;

        /// <summary>
        /// Horizontal position code. Specifies coodrinate frame to use when 
        /// paragraphs are absolutely positioned.<br/>
        /// 0 horizontal position coordinates are relative to column<br/>
        /// 1 coordinates are relative to column<br/>
        /// 2 coordinates are relative to page
        /// </summary>
        public byte pcHorz;

        /// <summary>
        /// Wrap code for absolute objects
        /// </summary>
        public byte wr;

        /// <summary>
        /// When fasle, text in paragraph may be auto hyphenated
        /// </summary>
        public bool fNoAutoHyph;

        /// <summary>
        /// Height when 0 == Auto
        /// </summary>
        public UInt16 wHeightAbs;

        /// <summary>
        /// Height of abs obj; 0 = Auto
        /// </summary>
        public UInt32 dyaHeight;

        /// <summary>
        /// Minimum height is exact or auto:<br/>
        /// false = exact
        /// true = at least
        /// </summary>
        public bool fMinHeight;

        /// <summary>
        /// Vertical distance between text and absolutely positioned object
        /// </summary>
        public Int32 dyaFromText;

        /// <summary>
        /// Horizontal distance between text and absolutely positioned object
        /// </summary>
        public Int32 dxaFromText;

        /// <summary>
        /// Anchor of an absolutely positioned frame is locked
        /// </summary>
        public bool fLocked;

        /// <summary>
        /// When true, Word will prevent windowed lines in this paragraph 
        /// from beeing placed at the begging of a page
        /// </summary>
        public bool fWindowControl;

        /// <summary>
        /// When true, appl Kinsoku rules when performing line wrapping
        /// </summary>
        public bool fKinsoku;

        /// <summary>
        /// When true, perform word wrap
        /// </summary>
        public bool fWordWrap;

        /// <summary>
        /// When true, apply overflow punctation rules when performing line wrapping
        /// </summary>
        public bool fOverflowPunct;

        /// <summary>
        /// When true, perform top line punctation
        /// </summary>
        public bool fTopLinePunct;

        /// <summary>
        /// When true, auto space East Asian and alphabetic characters
        /// </summary>
        public bool fAutoSpaceDE;

        /// <summary>
        /// When true, auto space East Asian and numeric characters
        /// </summary>
        public bool fAutoSpaceDN;

        /// <summary>
        /// Font alignment<br/>
        /// 0 Hanging<br/>
        /// 1 Centered<br/>
        /// 2 Roman<br/>
        /// 3 Variable<br/>
        /// 4 Auto
        /// </summary>
        public UInt16 wAlignFont;

        /// <summary>
        /// Used intenally by Word
        /// </summary>
        public bool fVertical;

        /// <summary>
        /// Used intenally by Word
        /// </summary>
        public bool fBackward;

        /// <summary>
        /// Used intenally by Word
        /// </summary>
        public bool fRotateFont;

        /// <summary>
        /// Reserved
        /// </summary>
        public Int16 empty;

        /// <summary>
        /// Outline level
        /// </summary>
        public byte lvl;

        /// <summary>
        /// 
        /// </summary>
        public bool fBiDi;

        /// <summary>
        /// Paragraph number is inserted (only valid if numrm.fNumRm is o)
        /// </summary>
        public bool fNumRMins;

        /// <summary>
        /// Used Internally
        /// </summary>
        public bool fCrLf;

        /// <summary>
        /// use page Setup Line Pitch
        /// </summary>
        public bool fUsePgsuSettings;

        /// <summary>
        /// Adjust right margin
        /// </summary>
        public bool AdjustRight;

        /// <summary>
        /// Table nesting level
        /// </summary>
        public Int32 itap;

        /// <summary>
        /// When true, he end of paragraph mark is really an end of 
        /// cell mark for a nested table cell
        /// </summary>
        public bool fInnerTableCell;

        /// <summary>
        /// Ensure the Table Cell char doesn't show up as zero height
        /// </summary>
        public bool fOpenTch;

        /// <summary>
        /// Right indent in character units
        /// </summary>
        public Int16 dxcRight;

        /// <summary>
        /// Left indent in character units
        /// </summary>
        public Int16 dxcLeft;

        /// <summary>
        /// First Lline indent in character units
        /// </summary>
        public Int16 dxcLeft1;

        /// <summary>
        /// Vertical spacing before paragraph in character units
        /// </summary>
        public Int16 dylBefore;

        /// <summary>
        /// Vertical spacing after paragraph in character units
        /// </summary>
        public Int16 dylAfter;

        /// <summary>
        /// Vertical spacing before is automatic
        /// </summary>
        public bool fDyaBeforeAuto;

        /// <summary>
        /// Vertical spacing after is automatic
        /// </summary>
        public bool fDyaAfterAuto;

        /// <summary>
        /// Word 97: indent from right margin<br/><br/>
        /// Word 2000: indent from right margin (signed) 
        /// for left-to-right text; from left margin for right-to-left text.
        /// </summary>
        public Int32 dxaRight;

        /// <summary>
        /// Word 97: indent from left margin<br/><br/>
        /// Word 2000: indent from left margin (signed) 
        /// for left-to-right text; from right margin for right-to-left text.
        /// </summary>
        public Int32 dxaLeft;

        /// <summary>
        /// First line indent; signed number relative to dxaLeft
        /// </summary>
        public Int32 dxaLeft1;

        /// <summary>
        /// Justification code<br/>
        /// Justification in Word 2000 and above is relative to text direction
        /// </summary>
        public JustificationCode jc;

        /// <summary>
        /// When true, properties have been changed with revision
        /// </summary>
        public bool fPropRMark;

        /// <summary>
        /// Used internally by Word
        /// </summary>
        public bool fCharLineUnits;

        /// <summary>
        /// Used internally by Word
        /// </summary>
        public bool fFrpTap;

        /// <summary>
        /// Used internally by Word
        /// </summary>
        public Int32 dxaFromTextRight;

        /// <summary>
        /// Used internally by Word
        /// </summary>
        public Int32 dyaFromTextBottom;

        /// <summary>
        /// Used internally by Word
        /// </summary>
        public Int32 lfrp;

        /// <summary>
        /// Number of tabs stops defined for paragraph.
        /// Must be &gt:= 0 and &lt;=64
        /// </summary>
        public Int16 itbdMac;

        /// <summary>
        /// Array of positions of itbdMa´c tab stops
        /// </summary>
        public Int16[] rgdxaTab;

        /// <summary>
        /// When true, absolutely positioned paragraph cannot 
        /// overlap with another paragraph
        /// </summary>
        public bool fNoAllowOverlap;

        /// <summary>
        /// HTML DIV ID for this paragraph
        /// </summary>
        public UInt32 ipgp;

        /// <summary>
        /// 
        /// </summary>
        public UInt32 rsid;
                
        /// <summary>
        /// Paragraph list style
        /// </summary>
        public UInt16 istdList;

        /// <summary>
        /// Used for paragraph property revision marking.<br/>
        /// The pap at the time fHasOldProps is true, the is the old pap.
        /// </summary>
        public bool fHasOldProps;

        /// <summary>
        /// Specification for border above paragraph
        /// </summary>
        public BorderCode brcTop;

        /// <summary>
        /// Specification for border to the left of paragraph
        /// </summary>
        public BorderCode brcLeft;

        /// <summary>
        /// Specification for border below paragraph
        /// </summary>
        public BorderCode brcBottom;

        /// <summary>
        /// Specification for border to the right of paragraph
        /// </summary>
        public BorderCode brcRight;

        /// <summary>
        /// Specification of border to place between conforming paragraphs.
        /// Two paragraphs conform when both have borders, their brcLeft and brcRight
        /// matches, their widths are the same, they both belong to tables or 
        /// both do not, and have the same absolute positioning props.
        /// </summary>
        public BorderCode brcBetween;

        /// <summary>
        /// Specification of border to place on outside of text when 
        /// facing pages are to be displayed
        /// </summary>
        public BorderCode brcBar;

        /// <summary>
        /// Line spacing descriptor for the paragraph
        /// </summary>
        public LineSpacingDescriptor lspd;

        /// <summary>
        /// Drop cap specifier
        /// </summary>
        public DropCapSpecifier dcs;

        /// <summary>
        /// Paragraph shading
        /// </summary>
        public ShadingDescriptor shd;

        /// <summary>
        /// Word 6.0 paragraph numbering
        /// </summary>
        public AutoNumberedListDataDescriptor anld;

        /// <summary>
        /// Height of current paragraph
        /// </summary>
        public ParagraphHeight phe;

        /// <summary>
        /// Date at which prperties of this were changed for 
        /// this run of text by the author.<br/>
        /// (Only recored when revision marking is on)
        /// </summary>
        public DateAndTime dttmPropMark;

        /// <summary>
        /// Array of itbdMac tab descriptors
        /// </summary>
        public TabDescriptor[] rgtbd;

        /// <summary>
        /// Paragraph numbering revision mark data
        /// </summary>
        public NumberRevisionMarkData numrm;

        /// <summary>
        /// Adjust right margin
        /// </summary>
        public bool fAdjustRight;

        /// <summary>
        /// Ignore the space before/after properties between paragraphs of the same style
        /// </summary>
        public bool fContextualSpacing;

        /// <summary>
        /// Creates a new ParagraphProperties with default values.
        /// </summary>
        public ParagraphProperties()
        {
            setDefaultValue();
        }

        /// <summary>
        /// Creates a ParagraphProperties of the paragraph that 
        /// starts at the given file character position
        /// </summary>
        /// <param name="fc">The file character position</param>
        /// <param name="fib">The fib</param>
        /// <param name="wordDocumentStream">The WordDocument stream</param>
        /// <param name="tableStream">The 0TableStream</param>
        public ParagraphProperties(Int32 fc, FileInformationBlock fib, VirtualStream wordDocumentStream, VirtualStream tableStream)
        {
            setDefaultValue();

            //get all FKPs
            List<FormattedDiskPagePAPX> papxFkps = FormattedDiskPagePAPX.GetAllPAPXFKPs(fib, wordDocumentStream, tableStream);
            foreach (FormattedDiskPagePAPX fkp in papxFkps)
            {
                for (int i = 0; i < fkp.grppapx.Length; i++)
                {
                    //ok, that's me?
                    if (fkp.rgfc[i] == fc)
                    {
                        //modify me
                        this.istd = fkp.grppapx[i].istd;
                        foreach(SinglePropertyModifier sprm in fkp.grppapx[i].grpprl)
                        {
                            this.Modify(sprm);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Modifies the ParagraphProperties object with a SinglePropertyModifier
        /// </summary>
        /// <param name="sprm">The SinglePropertyModifier</param>
        public void Modify(SinglePropertyModifier sprm)
        {
            //only modify the PAP if the sprm modifies a PAP
            if (sprm.Type == SinglePropertyModifier.SprmType.PAP)
            {
                switch (sprm.OpCode)
                {
                    case 0x4600:
                        this.istd = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0x2461:
                        this.jc = (JustificationCode)sprm.Arguments[0];
                        break;
                    case 0x2403:
                        this.jc = (JustificationCode)sprm.Arguments[0];
                        break;
                    case 0x2404:
                        this.fSideBySide = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2405:
                        this.fKeep = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2406:
                        this.fKeepFollow = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2407:
                        this.fPageBreakBefore = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2408:
                        this.brcl = sprm.Arguments[0];
                        break;
                    case 0x2409:
                        this.brcp = sprm.Arguments[0];
                        break;
                    case 0x260A:
                        this.ilvl = sprm.Arguments[0];
                        break;
                    case 0x460B:
                        this.ilfo = sprm.Arguments[0];
                        break;
                    case 0x240C:
                        this.fNoLynn = Utils.IntToBool((int)sprm.Arguments[0]);
                        break;
                    case 0xC60D:
                        //needs complex modification
                        break;
                    case 0x845e :
                        this.dxaLeft = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x840F:
                        this.dxaLeft = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x4456:
                        this.dxaLeft = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x465F:
                        this.dxaLeft = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x4610:
                        this.dxaLeft = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x8460:
                        this.dxaLeft1 = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x8411:
                        this.dxaLeft1 = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x4457:
                        this.dxaLeft1 = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x845D:
                        this.dxaRight = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x840E:
                        this.dxaRight = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x4455:
                        this.dxaRight = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x6412:
                        this.lspd = new LineSpacingDescriptor(sprm.Arguments);
                        break;
                    case 0xA413:
                        this.dyaBefore = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0xA414:
                        this.dyaAfter = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0x245C:
                        this.fDyaAfterAuto = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x245B:
                        this.fDyaBeforeAuto = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x4459:
                        this.dylAfter = System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x4458:
                        this.dylBefore = System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0xC615:
                        //needs complex modification
                        break;
                    case 0x2416:
                        this.fInTableW97 = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2417:
                        this.fTtp = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x8418:
                        this.dxaAbs = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x8419:
                        this.dyaAbs = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x841A:
                        this.dxaWidth = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x261B:
                        //needs complex handling;
                        break;
                    case 0x461C:
                        this.brcTop = new BorderCode(sprm.Arguments);
                        break;
                    case 0x461D:
                        this.brcLeft = new BorderCode(sprm.Arguments);
                        break;
                    case 0x461E:
                        this.brcBottom = new BorderCode(sprm.Arguments);
                        break;
                    case 0x461F:
                        this.brcRight = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4620:
                        this.brcBetween = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4621:
                        this.brcBar = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4622:
                        this.dxaFromText = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x2423:
                        this.wr = sprm.Arguments[0];
                        break;
                    case 0xC653:
                        this.brcBar = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4629:
                        this.brcBar = new BorderCode(sprm.Arguments);
                        break;
                    case 0x6629:
                        this.brcBar = new BorderCode(sprm.Arguments);
                        break;
                    case 0xC652:
                        this.brcBetween = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4428:
                        this.brcBetween = new BorderCode(sprm.Arguments);
                        break;
                    case 0x6428:
                        this.brcBetween = new BorderCode(sprm.Arguments);
                        break;
                    case 0xC650:
                        this.brcBottom = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4426:
                        this.brcBottom = new BorderCode(sprm.Arguments);
                        break;
                    case 0x6426:
                        this.brcBottom = new BorderCode(sprm.Arguments);
                        break;
                    case 0xC64F:
                        this.brcLeft = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4425:
                        this.brcLeft = new BorderCode(sprm.Arguments);
                        break;
                    case 0x6425:
                        this.brcLeft = new BorderCode(sprm.Arguments);
                        break;
                    case 0xC651:
                        this.brcRight = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4427:
                        this.brcRight = new BorderCode(sprm.Arguments);
                        break;
                    case 0x6427:
                        this.brcRight = new BorderCode(sprm.Arguments);
                        break;
                    case 0xC64E:
                        this.brcTop = new BorderCode(sprm.Arguments);
                        break;
                    case 0x4424:
                        this.brcTop = new BorderCode(sprm.Arguments);
                        break;
                    case 0x6424:
                        this.brcTop = new BorderCode(sprm.Arguments);
                        break;
                    case 0x242A:
                        this.fNoAutoHyph = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x442B:
                        this.wHeightAbs = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0x442c:
                        this.dcs = new DropCapSpecifier(sprm.Arguments);
                        break;
                    case 0x442D:
                        this.shd = new ShadingDescriptor(sprm.Arguments);
                        break;
                    case 0x842E:
                        this.dyaFromText =  (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x842F:
                        this.dxaFromText = (Int32)System.BitConverter.ToInt16(sprm.Arguments, 0);
                        break;
                    case 0x2430:
                        this.fLocked = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2431:
                        this.fWindowControl = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0xC632:
                        //not specified
                        break;
                    case 0x2433:
                        this.fKinsoku = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2434:
                        this.fWordWrap = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2435:
                        this.fOverflowPunct = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2436:
                        this.fTopLinePunct = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2437:
                        this.fAutoSpaceDE = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2438:
                        this.fAutoSpaceDN = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x4439:
                        this.wAlignFont = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0x443A:
                        //complex
                        break;
                    case 0x243B:
                        //obsolete
                        break;
                    case 0xC63E:
                        this.anld = new AutoNumberedListDataDescriptor(sprm.Arguments);
                        break;
                    case 0x6654:
                        this.anld.anlv.cv = System.BitConverter.ToInt32(sprm.Arguments, 0);
                        break;
                    case 0xC63F:
                        //complex;
                        break;
                    case 0x2640:
                        this.lvl = sprm.Arguments[0];
                        break;
                    case 0x2441:
                        this.fBiDi = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2443:
                        this.fNumRMins = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0xC645:
                        this.numrm = new NumberRevisionMarkData(sprm.Arguments);
                        break;
                    case 0x6645:
                        //pointer to the huge papx
                        break;
                    case 0x2447:
                        this.fUsePgsuSettings = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x2448:
                        this.fAdjustRight = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x664a:
                        this.itap = System.BitConverter.ToInt32(sprm.Arguments, 0);
                        if(this.itap == 0)
                            this.fInTableW97 = false;
                        else
                            this.fInTableW97 = true;
                        break;
                    case 0x244b:
                        this.fInnerTableCell = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x244c:
                        //see description
                        break;
                    case 0x2462:
                        this.fNoAllowOverlap = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x6649:
                        this.itap = System.BitConverter.ToInt32(sprm.Arguments, 0);
                        if(this.itap == 0)
                            this.fInTableW97 = false;
                        else
                            this.fInTableW97 = true;
                        break;
                    case 0x2664:
                        this.fHasOldProps = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x6465:
                        this.ipgp = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0xc666:
                        //
                        break;
                    case 0x6467:
                        this.rsid = System.BitConverter.ToUInt32(sprm.Arguments, 0);
                        break;
                    case 0x4468:
                        this.istdList = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0xc669:
                        this.istdList = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0xa46a:
                        this.dyaBefore = System.BitConverter.ToUInt16(sprm.Arguments, 0);
                        break;
                    case 0x646b:
                        //tab parsing is not yet implemented
                        break;
                    case 0xc66c:
                        //tab parsing is not yet implemented
                        break;
                    case 0x246D:
                        this.fContextualSpacing = Utils.ByteToBool(sprm.Arguments[0]);
                        break;
                    case 0x246E:
                        //revision pane flags
                        break;
                    case 0xC66F:
                        // needs complex handling
                        break;
                    default:
                        break;
                }
            }
        }

        private void setDefaultValue()
        {
            //The standard PAP is all zero ...
            this.AdjustRight = false;
            this.brcl = 0;
            this.brcp = 0;
            this.dxaAbs = 0;
            this.dxaFromText = 0;
            this.dxaFromTextRight = 0;
            this.dxaLeft = 0;
            this.dxaLeft1 = 0;
            this.dxaRight = 0;
            this.dxcLeft = 0;
            this.dxcLeft1 = 0;
            this.dxcRight = 0;
            this.dxaWidth = 0;
            this.dyaAbs = 0;
            this.dyaAfter = 0;
            this.dyaBefore = 0;
            this.dyaFromText = 0;
            this.dyaFromTextBottom = 0;
            this.dyaHeight = 0;
            this.dylAfter = 0;
            this.dylBefore = 0;
            this.empty = 0;
            this.fAutoSpaceDE = false;
            this.fAutoSpaceDN = false;
            this.fBackward = false;
            this.fBrLnAbove = false;
            this.fBrLnBelow = false;
            this.fCharLineUnits = false;
            this.fCrLf = false;
            this.fDyaAfterAuto = false;
            this.fDyaBeforeAuto = false;
            this.fFrpTap = false;
            this.fHasOldProps = false;
            this.fInnerTableCell = false;
            this.fInTableW97 = false;
            this.fKeep = false;
            this.fKeepFollow = false;
            this.fKinsoku = false;
            this.fLocked = false;
            this.fMinHeight = false;
            this.fNoAllowOverlap = false;
            this.fNoAutoHyph = false;
            this.fNoLynn = false;
            this.fNumRMins = false;
            this.fOpenTch = false;
            this.fOverflowPunct = false;
            this.fPageBreakBefore = false;
            this.fPropRMark = false;
            this.fRotateFont = false;
            this.fSideBySide = false;
            this.fTopLinePunct = false;
            this.fTtp = false;
            this.fUsePgsuSettings = false;
            this.fVertical = false;
            this.fWordWrap = false;
            this.ilfo = 0;
            this.ilvl = 0;
            this.ipgp = 0;
            this.istd = 0;
            this.istdList = 0;
            this.itap = 0;
            this.itbdMac = 0;
            this.jc = JustificationCode.left;
            this.lfrp = 0;
            this.pcHorz = 0;
            this.pcVert = 0;
            this.wAlignFont = 0;
            this.wHeightAbs = 0;
            this.wr = 0;
            this.anld = new AutoNumberedListDataDescriptor();
            this.brcBar = new BorderCode();
            this.brcBetween = new BorderCode();
            this.brcBottom = new BorderCode();
            this.brcLeft = new BorderCode();
            this.brcRight = new BorderCode();
            this.brcTop = new BorderCode();
            this.dcs = new DropCapSpecifier();
            this.dttmPropMark = new DateAndTime();
            this.numrm = new NumberRevisionMarkData();
            this.phe = new ParagraphHeight();
            this.rgdxaTab = Utils.ClearShortArray(new UInt16[64]);
            this.shd = new ShadingDescriptor();

            //except ...
            this.fWindowControl = true;
            this.lvl = 9;
            LineSpacingDescriptor desc = new LineSpacingDescriptor();
            desc.fMultLinespace = true;
            desc.dyaLine = 240;
            this.lspd = desc;
        }

        #region IVisitable Members

        public void Convert<T>(T mapping)
        {
            ((IMapping<ParagraphProperties>)mapping).Apply(this);
        }

        #endregion
    }
}
