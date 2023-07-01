using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockScopedNamespace;
using FileScopedNamespace;

namespace FileScopedNamespace
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockScopedNamespace;
internal class FileScopedNamespaceClass
{
}
}

internal class NoNamespaceClass
{
}

namespace BlockScopedNamespace
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileScopedNamespace;
    internal class BlockScopedNamespaceClass
    {
    }
}