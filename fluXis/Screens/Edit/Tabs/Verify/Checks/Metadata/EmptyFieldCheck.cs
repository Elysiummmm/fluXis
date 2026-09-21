using System.Collections.Generic;

namespace fluXis.Screens.Edit.Tabs.Verify.Checks.Metadata;

public class EmptyFieldCheck : IVerifyCheck
{
    public IEnumerable<VerifyIssue> Check(IVerifyContext ctx)
    {
        var map = ctx.Map;

        if (string.IsNullOrWhiteSpace(map.Title))
        {
            yield return new VerifyIssue(
                VerifyIssueSeverity.Problematic,
                VerifyIssueCategory.Metadata,
                null,
                "Title is empty."
            );
        }

        if (string.IsNullOrWhiteSpace(map.Artist))
        {
            yield return new VerifyIssue(
                VerifyIssueSeverity.Problematic,
                VerifyIssueCategory.Metadata,
                null,
                "Artist is empty."
            );
        }

        if (string.IsNullOrWhiteSpace(map.Difficulty))
        {
            yield return new VerifyIssue(
                VerifyIssueSeverity.Problematic,
                VerifyIssueCategory.Metadata,
                null,
                "Difficulty name is empty."
            );
        }
    }
}
