Spider Framework project is the core of Spider.Net. It contains the following 4 interface:

#1 IDocumentParser: An IDocumentParser finds the data area out, and identifies a small group of data in this area which contains data in a data row.

#2 IElementRowContentParser: An IElementRowContentParser parses a small group data to find cell value of a data row.

#3 IWebPageTurner: An IWebPageTurner turns web browser control to next page.

#4 IRelationProcessor: An IRelationProcess maintains the relations between two data tables.

With these interfaces, Spider Framework project implements a few common used parsers or turners. For example, a TableExtractor is an IDocumentParser whose responsibilities are to find a table out and to create data rows by table rows(tr). After it creates data rows, it passes the data row and table row to the IElementRowContentParser it hosts to extract each cell value(td) and save them to data row. Another example is that XPathRowContentParser is an IElementRowContentParser who gets the value of each data row by evaluating XPath, such as SPAN[0](0.md)/A[1](1.md) is to extract the value stored in the second Anchor child of first SPAN child of context node which is may be a table row or an element mapping to a data row.

With these implementations and interfaces, we may build a web crawler by them. It is easy to build your owned crawler..